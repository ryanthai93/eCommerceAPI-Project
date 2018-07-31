<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductDetail.aspx.vb" Inherits="ProductDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ps-product--detail pt-60">
        <asp:Label ID="lblError" class="alert alert-danger" runat="server" Text="Error"></asp:Label>
        <div class="ps-container">
          <div class="row">
                <div class="ps-product__thumbnail">
                    <div class="ps-product__image">
                      <div class="item"><img class="zoom" src="imgProduct" id="imgProduct" runat="server"></div>
                    </div>
                </div>
                
              <div class="ps-product__thumbnail--mobile">
                <div class="ps-product__main-img"><img src="images/shoe-detail/1.jpg" alt=""></div>
                <div class="ps-product__preview owl-slider" data-owl-auto="true" data-owl-loop="true" data-owl-speed="5000" data-owl-gap="20" data-owl-nav="true" data-owl-dots="false" data-owl-item="3" data-owl-item-xs="3" data-owl-item-sm="3" data-owl-item-md="3" data-owl-item-lg="3" data-owl-duration="1000" data-owl-mousedrag="on"><img src="images/shoe-detail/1.jpg" alt=""><img src="images/shoe-detail/2.jpg" alt=""><img src="images/shoe-detail/3.jpg" alt=""></div>
              </div>
              <div class="ps-product__info">
                
                <h1><asp:Label ID="lblProductName" runat="server" Text=""></asp:Label></h1>
                <p class="ps-product__category">Web ID: <asp:Label ID="lblProductNo" runat="server" Text=""></asp:Label></p>
                <h3 class="ps-product__price">$ <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label></h3>
                <div class="ps-product__block ps-product__quickview">

                  <p><asp:Label ID="lblProductDescription" runat="server" Text=""></asp:Label></p>
                </div>
                
                <div class="ps-product__block ps-product__size">
                        <label>Quantity:</label>
                        <input type="text" id="tbQuantity" runat="server" />                  
                        <asp:Button ID="btnAdd" runat="server" Text="Add to Cart" />
                </div>
 
              </div>
            </div>
          </div>
        </div>
      
</asp:Content>

