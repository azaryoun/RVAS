using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace RVAS_WinService
{
    public partial class RVAS : ServiceBase
    {

        private const string TARANOM_REST_BASE_IP_ADDRESS= "http://79.175.172.157:9001/";
        private const string TARANOM_REST_SEND_SERVICE= "taranom/transfer/send";
        private const string TARANOM_REST_COMPANY = "Rahyab";
        private const string TARANOM_REST_USERNAME = "rrgtest";
        private const string TARANOM_REST_PASSWORD = "rRgTeS7";

     
        public RVAS()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Threading.Thread trMonitorVASServices_NewsContent 
                = new System.Threading.Thread(MonitorVASServices_NewsContent);
            trMonitorVASServices_NewsContent.Start();

            System.Threading.Thread trMonitorVASServices_Serial
              = new System.Threading.Thread(MonitorVASServices_Serial);
            trMonitorVASServices_Serial.Start();


        }

        protected override void OnStop()
        {
        }

        private void MonitorVASServices_NewsContent()
        {
            BusinessObject.Context.dbVASEntities ctxVAS
                = new BusinessObject.Context.dbVASEntities();

         


            while (true) {
                try
                {
                    DateTime dteToday = DateTime.Now.Date;
                    TimeSpan spnFromTime = DateTime.Now.AddHours(-2).TimeOfDay;
                    TimeSpan spnToTime = DateTime.Now.TimeOfDay;

                    var intSendDateID = ctxVAS.tbl_Date.Where(x => x.DateG == dteToday).FirstOrDefault().ID;

                                      
                    var lnqVASServiceMembership_NewsContent = ctxVAS.tbl_VASServiceMembership_NewsContent.Where(x => x.tbl_NewsContent.SendDate==dteToday);
                    lnqVASServiceMembership_NewsContent = lnqVASServiceMembership_NewsContent.Where(x => x.tbl_NewsContent.SendTime >= spnFromTime && x.tbl_NewsContent.SendTime<=spnToTime);
                    lnqVASServiceMembership_NewsContent = lnqVASServiceMembership_NewsContent.Where(x => x.tbl_VASServiceMembership.IsNewsContent==true  && x.tbl_VASServiceMembership.tbl_VASService.IsActive==true);

                    var lstVASServiceMembership_NewsContent = lnqVASServiceMembership_NewsContent.ToList();


                    if (lstVASServiceMembership_NewsContent.Count == 0)
                    {
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }

                    foreach (var itemVASServiceMembership_NewsContent in lstVASServiceMembership_NewsContent)
                    {
                        var lstVASServiceMembershipSubscriber = ctxVAS.tbl_VASServiceMembershipSubscriber.Where(x => x.FK_VASServiceID == itemVASServiceMembership_NewsContent.FK_VASServiceID && x.UnsubscriptionDate == null).ToList();
                        foreach (var itemVASServiceMembershipSubscriber in lstVASServiceMembershipSubscriber)
                        {
                            var blnSendLog = ctxVAS.tbl_SendLog.Where(x => x.FK_VASServiceID == itemVASServiceMembership_NewsContent.FK_VASServiceID && x.FK_VASMembershipSubscriberID == itemVASServiceMembershipSubscriber.ID && x.FK_ID == itemVASServiceMembership_NewsContent.FK_NewsContentID).Any();
                            if (blnSendLog == true)
                                continue;
                            
                            var rowSendLog = new BusinessObject.Context.tbl_SendLog();

                            try
                            {
                                //Call Send Service Here

                                rowSendLog.FK_ID = itemVASServiceMembership_NewsContent.FK_NewsContentID;
                                rowSendLog.FK_SendDateID = intSendDateID;
                                rowSendLog.FK_VASMembershipSubscriberID = itemVASServiceMembershipSubscriber.ID;
                                rowSendLog.FK_VASServiceID = itemVASServiceMembership_NewsContent.FK_VASServiceID;
                                rowSendLog.ReceiverMobile = itemVASServiceMembershipSubscriber.SubscriberMobileNo;
                                rowSendLog.SendDateTime = DateTime.Now;
                                rowSendLog.SerialOrder = null;
                                rowSendLog.ServicePrice = itemVASServiceMembership_NewsContent.tbl_VASServiceMembership.tbl_VASService.ServicePrice;
                                rowSendLog.theStatus = 1; //Success
                                rowSendLog.theText = itemVASServiceMembership_NewsContent.tbl_NewsContent.theText;
                            }
                            catch
                            {
                                rowSendLog.FK_ID = itemVASServiceMembership_NewsContent.FK_NewsContentID;
                                rowSendLog.FK_SendDateID = intSendDateID;
                                rowSendLog.FK_VASMembershipSubscriberID = itemVASServiceMembershipSubscriber.ID;
                                rowSendLog.FK_VASServiceID = itemVASServiceMembership_NewsContent.FK_VASServiceID;
                                rowSendLog.ReceiverMobile = itemVASServiceMembershipSubscriber.SubscriberMobileNo;
                                rowSendLog.SendDateTime = DateTime.Now;
                                rowSendLog.SerialOrder = null;
                                rowSendLog.ServicePrice = itemVASServiceMembership_NewsContent.tbl_VASServiceMembership.tbl_VASService.ServicePrice;
                                rowSendLog.theStatus = 0; //Failed
                                rowSendLog.theText = itemVASServiceMembership_NewsContent.tbl_NewsContent.theText;

                            }

                            ctxVAS.tbl_SendLog.Add(rowSendLog);
                            ctxVAS.SaveChanges();


                        }
                                           

                    }// Main For

                }
                catch
                {
                    System.Threading.Thread.Sleep(5000);
                    continue;
                }


            }


        }

        private void MonitorVASServices_Serial()
        {
            BusinessObject.Context.dbVASEntities ctxVAS
               = new BusinessObject.Context.dbVASEntities();
            
           while (true)
            {
                try
                {

                    DateTime dteToday = DateTime.Now.Date;
                    var intSendDateID = ctxVAS.tbl_Date.Where(x => x.DateG == dteToday).FirstOrDefault().ID;

                    var lnqVASServiceMembershipSerialContentHeader = ctxVAS.tbl_VASServiceMembershipSerialContentHeader.Where(x => x.IsActive == true && x.StratFrom <= dteToday && x.EndAt >= dteToday && x.tbl_VASServiceMembership.IsNewsContent == false && x.tbl_VASServiceMembership.tbl_VASService.IsActive == true);

                    var lstVASServiceMembershipSerialContentHeader = lnqVASServiceMembershipSerialContentHeader.ToList();

                    if (lstVASServiceMembershipSerialContentHeader.Count == 0)
                    {
                        System.Threading.Thread.Sleep(5000);
                        continue;
                    }

                    foreach (var itemVASServiceMembershipSerialContentHeader in lstVASServiceMembershipSerialContentHeader)
                    {
                        var lstVASServiceMembershipSubscriber = ctxVAS.tbl_VASServiceMembershipSubscriber.Where(x => x.FK_VASServiceID == itemVASServiceMembershipSerialContentHeader.FK_VASServiceID && x.UnsubscriptionDate == null).ToList();
                        foreach (var itemVASServiceMembershipSubscriber in lstVASServiceMembershipSubscriber)
                        {

                            var lnqSendLog = ctxVAS.tbl_SendLog.Where(x => x.FK_VASServiceID == itemVASServiceMembershipSerialContentHeader.FK_VASServiceID && x.FK_VASMembershipSubscriberID == itemVASServiceMembershipSubscriber.ID).OrderByDescending(x => x.SerialOrder).FirstOrDefault();

                            if (lnqSendLog == null)
                            {
                                var lnqVASServiceMembershipSerialContentFooter = ctxVAS.tbl_VASServiceMembershipSerialContentFooter.Where(x => x.FK_VASServiceID == itemVASServiceMembershipSerialContentHeader.FK_VASServiceID && x.theOrder == 1).FirstOrDefault();
                                if (lnqVASServiceMembershipSerialContentFooter == null)
                                    continue;
                                DateTime dteBaseDate = itemVASServiceMembershipSubscriber.SubscrptionDate.Value;
                                if (DateTime.Now.Subtract(dteBaseDate).TotalHours >= lnqVASServiceMembershipSerialContentFooter.IntervalDay && lnqVASServiceMembershipSerialContentFooter.SendTime >= DateTime.Now.TimeOfDay)
                                {
                                    var rowSendLog = new BusinessObject.Context.tbl_SendLog();

                                    try
                                    {
                                        //Call Send Service Here

                                        rowSendLog.FK_ID = lnqVASServiceMembershipSerialContentFooter.ID;
                                        rowSendLog.FK_SendDateID = intSendDateID;
                                        rowSendLog.FK_VASMembershipSubscriberID = itemVASServiceMembershipSubscriber.ID;
                                        rowSendLog.FK_VASServiceID = itemVASServiceMembershipSerialContentHeader.FK_VASServiceID;
                                        rowSendLog.ReceiverMobile = itemVASServiceMembershipSubscriber.SubscriberMobileNo;
                                        rowSendLog.SendDateTime = DateTime.Now;
                                        rowSendLog.SerialOrder = 1;
                                        rowSendLog.ServicePrice = itemVASServiceMembershipSerialContentHeader.tbl_VASServiceMembership.tbl_VASService.ServicePrice;
                                        rowSendLog.theStatus = 1; //Success
                                        rowSendLog.theText = lnqVASServiceMembershipSerialContentFooter.theText;
                                    }
                                    catch
                                    {
                                        rowSendLog.FK_ID = lnqVASServiceMembershipSerialContentFooter.ID;
                                        rowSendLog.FK_SendDateID = intSendDateID;
                                        rowSendLog.FK_VASMembershipSubscriberID = itemVASServiceMembershipSubscriber.ID;
                                        rowSendLog.FK_VASServiceID = itemVASServiceMembershipSerialContentHeader.FK_VASServiceID;
                                        rowSendLog.ReceiverMobile = itemVASServiceMembershipSubscriber.SubscriberMobileNo;
                                        rowSendLog.SendDateTime = DateTime.Now;
                                        rowSendLog.SerialOrder = 1;
                                        rowSendLog.ServicePrice = itemVASServiceMembershipSerialContentHeader.tbl_VASServiceMembership.tbl_VASService.ServicePrice;
                                        rowSendLog.theStatus = 0; //failed
                                        rowSendLog.theText = lnqVASServiceMembershipSerialContentFooter.theText;

                                    }

                                    ctxVAS.tbl_SendLog.Add(rowSendLog);
                                    ctxVAS.SaveChanges();

                                }
                                else
                                    continue;

                            } //lnqSendLog == null

                            else  //lnqSendLog is not  null
                            {
                                var lnqVASServiceMembershipSerialContentFooter = ctxVAS.tbl_VASServiceMembershipSerialContentFooter.Where(x => x.FK_VASServiceID == itemVASServiceMembershipSerialContentHeader.FK_VASServiceID && x.theOrder == lnqSendLog.SerialOrder + 1).FirstOrDefault();
                                if (lnqVASServiceMembershipSerialContentFooter == null)
                                    continue;
                                DateTime dteBaseDate = lnqSendLog.SendDateTime.Value;
                               


                                if (DateTime.Now.Subtract(dteBaseDate).TotalHours >= lnqVASServiceMembershipSerialContentFooter.IntervalDay && lnqVASServiceMembershipSerialContentFooter.SendTime >= DateTime.Now.TimeOfDay)
                                {
                                    var rowSendLog = new BusinessObject.Context.tbl_SendLog();
                                    int intCurrentSerialOrder = lnqSendLog.SerialOrder.Value + 1;

                                    try
                                    {
                                        //Call Send Service Here

                                        rowSendLog.FK_ID = lnqVASServiceMembershipSerialContentFooter.ID;
                                        rowSendLog.FK_SendDateID = intSendDateID;
                                        rowSendLog.FK_VASMembershipSubscriberID = itemVASServiceMembershipSubscriber.ID;
                                        rowSendLog.FK_VASServiceID = itemVASServiceMembershipSerialContentHeader.FK_VASServiceID;
                                        rowSendLog.ReceiverMobile = itemVASServiceMembershipSubscriber.SubscriberMobileNo;
                                        rowSendLog.SendDateTime = DateTime.Now;
                                        rowSendLog.SerialOrder = intCurrentSerialOrder;
                                        rowSendLog.ServicePrice = itemVASServiceMembershipSerialContentHeader.tbl_VASServiceMembership.tbl_VASService.ServicePrice;
                                        rowSendLog.theStatus = 1; //Success
                                        rowSendLog.theText = lnqVASServiceMembershipSerialContentFooter.theText;
                                    }
                                    catch
                                    {
                                        rowSendLog.FK_ID = lnqVASServiceMembershipSerialContentFooter.ID;
                                        rowSendLog.FK_SendDateID = intSendDateID;
                                        rowSendLog.FK_VASMembershipSubscriberID = itemVASServiceMembershipSubscriber.ID;
                                        rowSendLog.FK_VASServiceID = itemVASServiceMembershipSerialContentHeader.FK_VASServiceID;
                                        rowSendLog.ReceiverMobile = itemVASServiceMembershipSubscriber.SubscriberMobileNo;
                                        rowSendLog.SendDateTime = DateTime.Now;
                                        rowSendLog.SerialOrder = intCurrentSerialOrder;
                                        rowSendLog.ServicePrice = itemVASServiceMembershipSerialContentHeader.tbl_VASServiceMembership.tbl_VASService.ServicePrice;
                                        rowSendLog.theStatus = 0; //failed
                                        rowSendLog.theText = lnqVASServiceMembershipSerialContentFooter.theText;

                                    }

                                    ctxVAS.tbl_SendLog.Add(rowSendLog);
                                    ctxVAS.SaveChanges();

                                }
                                else
                                    continue;


                            } //else





                        }//  For itemVASServiceMembershipSubscriber

                    }// For itemVASServiceMembershipSerialContentHeader

                }
                catch
                {
                    System.Threading.Thread.Sleep(5000);
                    continue;
                }


            } //While


        } // Void MonitorVASServices_Serial()




        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(TARANOM_REST_BASE_IP_ADDRESS);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var strTo = "989122764983";
                var strFrom = "9830834911";
                var strServiceID = "09118a098cbd4013bb6775c607869034";
                var strMessage = "The Message";

             //   var oSMS = new {sc= TARANOM_REST_COMPANY, to= strTo, from= strFrom, serviceId= strServiceID, username=TARANOM_REST_USERNAME, password= TARANOM_REST_PASSWORD, message =  strMessage};
               
                HttpResponseMessage response = null;
                var strURL = string.Format("{0}?sc={1}&to={2}&from={3}&serviceId={4}&username={5}&password={6}&message={7}", TARANOM_REST_SEND_SERVICE, TARANOM_REST_COMPANY, strTo, strFrom,strServiceID, TARANOM_REST_USERNAME, TARANOM_REST_PASSWORD,strMessage);

                response = await client.GetAsync(strURL);

                if (response.IsSuccessStatusCode)
                {
                    string strVAS = await response.Content.ReadAsStringAsync();
                }


            }
        }
    }
}
