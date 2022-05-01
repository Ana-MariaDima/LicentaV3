import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { environment } from 'src/environments/environment';
import { ModalPopupPageRet } from '../modal-popup-ret/modal-popup-ret.page';
import { ReteteService } from '../services/Retete/retete.service';
import { StarsComponent } from '../stars/stars.component';
@Component({
  selector: 'app-tab1-toate-retetele',
  templateUrl: './tab1-toate-retetele.page.html',
  styleUrls: ['./tab1-toate-retetele.page.scss'],
})
export class Tab1ToateRetetelePage  {
  @Input() liked: any;
  @Input() model_title: string;
  searchTerm: string;




  constructor(private ReteteService: ReteteService,private modalController: ModalController, private reteteService: ReteteService, private starsC:StarsComponent) { }
  retete:Array<any> = []
  retete_all:Array<any> = []
  tip:Array<any> = []
  currentPage:number = 1
  recordsPerPage:number = 100;
  hasReachedEnd:boolean = false;

  async ngOnInit() {
    this.currentPage = 1;
    this.retete = [];
    this.retete_all = [];
    environment.baseUrl
    this.retete = (await this.ReteteService.getRetete(true));

    this.ReteteService.getRetete(false).then(results=>{
      this.retete = this.retete.concat(results);
    })

    this.hasReachedEnd = this.retete.length == 0;

    this.retete_all=(await this.ReteteService.allRetete());

    //console.log(this.retete_all)
  }

  async openCardModal(reteta){
     //await Tab1ToateRetetelePage.openRetetaModal(reteta);
  }

  async openRetetaModal(reteta){
    console.log("Reteta deschisa", reteta)


    var modal = await this.modalController.create({
      component:ModalPopupPageRet,
      cssClass:"modalTest",
      componentProps: {
        'model_title':reteta.nume_reteta,
       'tipReteta': reteta.nume_Tip_Retete,
        'instructiuni':reteta.instructiuni ,
        'pahar':reteta.nume_pahar ,
        'poza':reteta.poza_reteta,
        'raiting': reteta.rating_retea,
        'ingrediente':reteta.retetaIngredient,
        'liked':  reteta.liked,
        'categorieReteta': reteta.nume_Categorie_Retete
      }
    })


    modal.present()
    const {data} = await modal.onWillDismiss();
   // console.log("modal returned data", data)

  }


  toggleLiked(reteta) {


    //console.log(this)
    reteta.liked = !reteta.liked;
    this.reteteService.toggleLike(reteta.nume_reteta);

  }
}

