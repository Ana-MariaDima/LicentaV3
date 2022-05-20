import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ReteteService } from '../services/Retete/retete.service';

@Component({
  selector: 'app-modal-popup-cart',
  templateUrl: './modal-popup-cart.page.html',
  styleUrls: ['./modal-popup-cart.page.scss'],
})
export class ModalPopupCartPage implements OnInit {
  items:Array<any>
  itemsInCart: any[]=[];
  buttonDisabled:boolean =false;
  constructor(private modalController:ModalController, private reteteService: ReteteService) {

  }

  ngOnInit() {
    if(localStorage.getItem("cart") != undefined)
    {
    this.itemsInCart = Object.values(JSON.parse(localStorage.getItem('cart')))
    console.log("Items in cart", this.itemsInCart);
    }
  }


  removeFromCart(ingredient){
    var newCart = {}
    this.itemsInCart.forEach(x=>{
      if(x.id != ingredient.id){
        newCart[x.id] = x
      }
    });
    console.log("before delete ", this.itemsInCart.length);
    this.itemsInCart = Object.values(newCart);

    console.log("deleted ", this.itemsInCart.length)

    localStorage.setItem('cart', JSON.stringify(newCart))
  }

  async dismiss() {
    const close: string = "Modal Removed";
    return this.modalController.dismiss({customProp:"123"});
  }

  genertaePerfectMatch(){
    if(localStorage.getItem("cart") != undefined)
    {
    this.itemsInCart = Object.values(JSON.parse(localStorage.getItem('cart')))
    console.log('items in cart now', this.itemsInCart)
    }

    this.buttonDisabled = true;
    setTimeout(x=>{
      this.modalController.dismiss();
      if(window.location.href.includes("generate")){
        window.location.reload();
      }

    },500);

  }

}
