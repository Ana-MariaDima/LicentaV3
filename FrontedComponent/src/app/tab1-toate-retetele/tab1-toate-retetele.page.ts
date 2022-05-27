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

 typeFilterArr:any = [];
 nameFilterArr:any = [];




  constructor(private ReteteService: ReteteService,private modalController: ModalController, private reteteService: ReteteService) { }
  retete:Array<any> = []
  retete_all:Array<any> = []
  tip:Array<any> = []
  currentPage:number = 1
  recordsPerPage:number = 100;
  hasReachedEnd:boolean = false;
  tipuriRetete:Array<any> = []
  categoriiRetete: Array<any> = []
  initialOrder:any = [];

  async ngOnInit() {
    this.currentPage = 1;
    this.retete = [];
    this.retete_all = [];
    this.categoriiRetete= await this.ReteteService.getCategoriiRetete();

    this.tipuriRetete= await this.ReteteService.getTipuriRetete();
   console.log("categorii",this.categoriiRetete)
    environment.baseUrl
    this.retete = (await this.ReteteService.getRetete(true));

    this.ReteteService.getRetete(false).then(results=>{
      this.retete = this.retete.concat(results);
      this.initialOrder = this.retete.map(x=>x.id)
    })

    this.hasReachedEnd = this.retete.length == 0;
    console.log("retete all", this.retete)

    this.retete_all=(await this.ReteteService.allRetete());

    (window as any).EventSystem.listen('retetaUnliked', (retetaUnliked)=>{
        var idx = this.retete.findIndex(r=>
          r.nume_reteta == retetaUnliked.nume_reteta
        );

        this.retete[idx].liked = false;
        this.retete[idx].nr_likes = this.retete[idx].nr_likes - 1 < 0 ? 0: this.retete[idx].nr_likes-1;
        console.log("Reteta unliked ", retetaUnliked)
    });

    (window as any).EventSystem.listen('retetaReviewed', (ret)=>{
      var idx = this.retete.findIndex(r=>
        r.nume_reteta == ret.nume_reteta
      );


      this.retete[idx].rating = ret.rating;
      this.retete[idx].user_rating = ret.user_rating;
    });

    //console.log(this.retete_all)
  }

  async sort(ev){
    var criteriu = ev.detail.value;
    const criteriiSelectie = {
      'nrLikesAsc': function(a,b){
        return a.nr_likes - b.nr_likes //sorteaza crescator dupa likeuri
      },
      'nrLikesDesc':function(a,b){
        return  b.nr_likes - a.nr_likes //sorteaza desc dupa likeuri
      },
      'default':(a,b) => {
        return this.initialOrder.indexOf( a.id ) - this.initialOrder.indexOf( b.id );

      },
      'ScorAsc':function(a,b){
        return  a.rating - b.rating //sorteaza asc dupa scor
      },
      'ScorDesc':function(a,b){
        return  b.rating - a.rating //sorteaza desc dupa scor
      }
    }
    var functieDeCriteriu = criteriiSelectie[criteriu]
    console.log(ev, criteriu, functieDeCriteriu)

    this.retete.sort(functieDeCriteriu);
  }

  async typeFilter(ev){
    console.log(ev);
    this.typeFilterArr = ev.detail.value;
    console.log(this.retete, this.typeFilterArr);
    this.retete.forEach(x=>x.hidden = true);

    if(this.nameFilterArr.length ==0){
    this.retete.filter(x=> this.typeFilterArr.length == 0
                           || this.typeFilterArr.indexOf(x.nume_Tip_Retete) != -1

                )
                .forEach(x=>x.hidden = false);//&& this.nameFilterArr.indexOf(x.nume_Categorie_Retete) == -1)
    }else{
      var reteteFiltrateTip = this.retete.filter(x=> this.typeFilterArr.length == 0
        || this.typeFilterArr.indexOf(x.nume_Tip_Retete) != -1).map(x=>x.id);
      var reteteFiltrateCategorie = this.retete.filter(x=> this.nameFilterArr.length == 0
        || this.nameFilterArr.indexOf(x.nume_Categorie_Retete) != -1).map(x=>x.id);

      var idsDeAfisat = reteteFiltrateTip.filter(id=> reteteFiltrateCategorie.indexOf(id) !== -1);

      this.retete.forEach(x=>{
         if(idsDeAfisat.indexOf(x.id) !== -1){
            x.hidden = false;
         }else{
           x.hidden = true;
         }
      });
    }


  }

  async nameFilter(ev){
    this.nameFilterArr = ev.detail.value;

    this.retete.forEach(x=>x.hidden = true);

    if(this.typeFilterArr.length ==0){
    this.retete.filter(x=> this.nameFilterArr.length == 0
                           || this.nameFilterArr.indexOf(x.nume_Tip_Retete) != -1

                )
                .forEach(x=>x.hidden = false);
    }else{
      var reteteFiltrateCategorie = this.retete.filter(x=> this.nameFilterArr.length == 0
        || this.nameFilterArr.indexOf(x.nume_Categorie_Retete) != -1).map(x=>x.id);
      var reteteFiltrateTip = this.retete.filter(x=> this.typeFilterArr.length == 0
        || this.typeFilterArr.indexOf(x.nume_Tip_Retete) != -1).map(x=>x.id);

      var idsDeAfisat = reteteFiltrateTip.filter(id=> reteteFiltrateCategorie.indexOf(id) !== -1);

      this.retete.forEach(x=>{
         if(idsDeAfisat.indexOf(x.id) !== -1){
            x.hidden = false;
         }else{
           x.hidden = true;
         }
      });
    }
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
        'rating': reteta.rating,
        'ingrediente':reteta.retetaIngredient,
        'liked':  reteta.liked,
        'categorieReteta': reteta.nume_Categorie_Retete,
        'nr_likes':reteta.nr_likes,
        'user_rating':reteta.user_rating,

      }
    })

    modal.present()
    var {data} = await modal.onWillDismiss();
    console.log(data, "data all")

    data = JSON.parse(data.payload);
    if(data.nr_likes!= reteta.nr_likes ){
      this.toggleLiked(reteta);
    }

    console.log("Modal closed !", data);
    //if(data.user_rating != reteta.user_rating)
    reteta.user_rating = data.user_rating;
    reteta.rating = data.rating;

    var idx = this.retete.findIndex(x=>{
      x.nume_reteta == reteta.nume_reteta
    });

    //this.retete[idx]  // = fetch()
    // var reteteTmp = this.retete;
    // this.retete= []
    // setTimeout(()=>{
    //   this.retete = reteteTmp
    // },100)
    //this.retete = reteteTmp;
   // console.log("modal returned data", data)


}


  toggleLiked(reteta) {
    reteta.liked = !reteta.liked;
    if(reteta.liked ==true)
    reteta.nr_likes=reteta.nr_likes+1;
    else
    reteta.nr_likes=reteta.nr_likes-1;

    this.reteteService.toggleLike(reteta.nume_reteta);
    console.log("before fetched");
    this.reteteService.fetchReteteApreciate();
  }

  onReview(reteta){

    return async (review)=>{
      var rezultat =  await this.submitReview(reteta.nume_reteta, review);
      reteta.user_rating = review;
      reteta.rating = rezultat.medie;
    }
  }

  async submitReview(reteta, review){
    var nouaMedie = await this.reteteService.submitReview(reteta, review);

    return nouaMedie
  }
}

