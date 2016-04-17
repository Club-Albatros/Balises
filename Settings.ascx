<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Albatros.DNN.Modules.Balises.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<fieldset>
 <div class="dnnFormItem">
  <dnn:label id="lblView" runat="server" controlname="ddView" suffix=":" />
  <asp:DropDownList runat="server" ID="ddView" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="lblGoogleMapApiKey" runat="server" controlname="txtGoogleMapApiKey" suffix=":" />
  <asp:TextBox ID="txtGoogleMapApiKey" runat="server" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="lblBeaconPassDistanceMeters" runat="server" controlname="txtBeaconPassDistanceMeters" suffix=":" />
  <asp:TextBox ID="txtBeaconPassDistanceMeters" runat="server" />
 </div>
</fieldset>
