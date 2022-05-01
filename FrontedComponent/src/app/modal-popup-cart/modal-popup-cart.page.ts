import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-modal-popup-cart',
  templateUrl: './modal-popup-cart.page.html',
  styleUrls: ['./modal-popup-cart.page.scss'],
})
export class ModalPopupCartPage implements OnInit {
  items:Array<any>
  itemsInCart: any[]=[];

  constructor(private modalController:ModalController) {

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
    }
    console.log(this.itemsInCart)

  }

}
