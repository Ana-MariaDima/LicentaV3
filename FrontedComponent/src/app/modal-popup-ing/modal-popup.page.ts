import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalServiceService } from '../modal-service.service';
import { Tab1PageModule } from '../tab1/tab1.module';
import { Tab1Page } from '../tab1/tab1.page';

@Component({
  selector: 'app-modal-popup',
  templateUrl: './modal-popup.page.html',
  styleUrls: ['./modal-popup.page.scss'],
})
export class ModalPopupPage implements OnInit {
  @Input() model_title: string;
  @Input() subcategorie: any;
  @Input() ingrediente: any;

  searchTerm: string;
  itemsInCart: any[]=[];
  selected:any = {};
  constructor(
    private modalController: ModalController,
    private modalService:ModalServiceService,



  ) {


   }

  refreshCart(){
    this.itemsInCart = Object.values(JSON.parse(localStorage.getItem('cart')))
    this.ingrediente.forEach(x=>{ this.selected[x.id] = this.itemsInCart.map(z=>z.id).includes(x.id);});

  }
  ngOnInit() {
    if(localStorage.getItem("cart") != undefined)
    {
      this.refreshCart()
    }
}

  addtoCart(item)
  {
    this.selected[item.id] = true;
    item.quantityInCart += 1;
    this.itemsInCart.push(item);
    //console.log( item.quantityInCart);
    var cart = localStorage.getItem('cart')? JSON.parse(localStorage.getItem('cart')):{}
    cart[item.id] = item
    localStorage.setItem('cart', JSON.stringify(cart));

  }

  removeFromCart(item){
    this.selected[item.id] = false;
    var newCart = {}
    this.itemsInCart.forEach(x=>{
      if(x.id != item.id){
        newCart[x.id] = x
      }
    });
   // this.itemsInCart=newCart;
   item.quantityInCart -= 1;
    this.itemsInCart = Object.values(newCart);

    //console.log("deleted ",item, this.itemsInCart)

    localStorage.setItem('cart', JSON.stringify(newCart))
  }

  async dismiss() {
    const close: string = "Modal Removed";

    //this.tab1.ionViewWillEnter();

    console.log("popup dismissing!")
     var result= this.modalController.dismiss({customProp:"123"});

     return result
  }

  async openCart(){
    this.modalController.dismiss();
    await this.modalService.openCartModal()
    this.refreshCart();

 }
}
