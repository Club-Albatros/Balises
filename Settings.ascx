<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Albatros.DNN.Modules.Balises.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<fieldset>
 <div class="dnnFormItem">
  <dnn:Label ID="lblMySetting" runat="server" controlname="txtMySetting" suffix=":" />
  <asp:TextBox ID="txtMySetting" runat="server" />
 </div>
</fieldset>

