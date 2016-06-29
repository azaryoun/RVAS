<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Smart.Master" CodeBehind="StartPage.aspx.vb" Inherits="RVAS.StartPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <section class="content">
               <br />
               <br />
                  
                    <div class="row">
                        <div class="col-lg-3 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-red">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblServiceCountSystematic" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد سرویس های سیستمی</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-laptop"></i>
                                </div>
                                <a href="/Application/Report/OnDemand/Systematic/OnDemandSystematicSendReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-3 col-xs-6">
                               <div class="small-box bg-green">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblServiceCountManual" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد سرویس های دستی</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-hand-pointer-o"></i>
                                </div>
                                <a href="/Application/Report/OnDemand/Manual/OnDemandManualSendReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>
                        <!-- ./col -->
                     <div class="col-lg-3 col-xs-6">
                               <div class="small-box bg-yellow">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblServiceCountSerial" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد سرویس های سریالی</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-link"></i>
                                </div>
                                <a href="/Application/Report/Membership/Serial/MembershipSerialSendReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>

                        <!-- ./col -->
                         <div class="col-lg-3 col-xs-6">
                               <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblServiceCountNews" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد سرویس های خبری</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-newspaper-o"></i>
                                </div>
                                <a href="/Application/Report/Membership/News/MembershipNewsSendReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>
                        <!-- ./col -->
                    </div>
               <div class="row">
                        <div class="col-lg-3 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-red-active">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSendCountSystematic" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد ارسال امروز سیستمی</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-paper-plane-o"></i>
                                </div>
                                <a href="/Application/Report/OnDemand/Systematic/OnDemandSystematicSendReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-3 col-xs-6">
                               <div class="small-box bg-green-active">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSendCountManual" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد ارسال امروز دستی</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-paper-plane-o"></i>
                                </div>
                                <a href="/Application/Report/OnDemand/Manual/OnDemandManualSendReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>
                        <!-- ./col -->
                     <div class="col-lg-3 col-xs-6">
                               <div class="small-box bg-yellow-active">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSendCountSerial" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد ارسال امروز سریالی</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-paper-plane-o"></i>
                                </div>
                                <a href="/Application/Report/Membership/Serial/MembershipSerialSendReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>

                        <!-- ./col -->
                         <div class="col-lg-3 col-xs-6">
                               <div class="small-box bg-aqua-active">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSendCountNews" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد ارسال امروز خبری</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-paper-plane-o"></i>
                                </div>
                                <a href="/Application/Report/Membership/News/MembershipNewsSendReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>
                        <!-- ./col -->
                    </div>

            <br />
                <div class="row">
                    
                        <div class="col-lg-6 col-xs-6">
                               <div class="small-box bg-yellow-gradient">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSubscriberCountSerial" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد اعضای سرویس های سریالی</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="/Application/Report/Membership/Serial/MembershipSerialMembersReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>

                        <!-- ./col -->
                         <div class="col-lg-6 col-xs-6">
                               <div class="small-box bg-aqua-gradient">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSubscriberCountNews" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد اعضای سرویس های خبری</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="/Application/Report/Membership/News/MembershipNewsMembersReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>
                    
                        <!-- ./col -->
                    </div>
       
                <div class="row">
                    
                        <div class="col-lg-6 col-xs-6">
                               <div class="small-box bg-orange">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSubscriptionSerial" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد کل اشتراک های سرویس های سریالی</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-sign-in"></i>
                                </div>
                                <a href="/Application/Report/Membership/Serial/MembershipSerialSubscriptionReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>

                        <!-- ./col -->
                         <div class="col-lg-6 col-xs-6">
                               <div class="small-box bg-teal-active">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSubscriptionNews" runat="server" Text="0"></asp:Label></h3>
                                    <p>تعداد کل اشتراک های سرویس های خبری</p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-sign-in"></i>
                                </div>
                                <a href="/Application/Report/Membership/News/MembershipNewsSubscriptionReport.aspx" class="small-box-footer" style="font-size:11px"><i class="fa fa-arrow-circle-right"></i> اطلاعات بیشتر </a>
                            </div>


                        </div>
                    
                        <!-- ./col -->
                    </div>
       
       
            

                </section>

</asp:Content>
