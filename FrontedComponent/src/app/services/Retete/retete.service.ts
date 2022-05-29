import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReteteService {




  constructor(private http:HttpClient) { }

    private groupBy(array:Array<any>){
      var retete = {}
      array.forEach(item=>{
        var idReteta = item['id']
        if(!retete[idReteta]){
          retete[idReteta] = {}
        }
        var retetaCurenta = retete[idReteta];

        if(!retetaCurenta.nume_reteta){
          retetaCurenta.nume_reteta = item['nume_reteta']
          retetaCurenta.nume_Tip_Retete = item['nume_Tip_Retete']
          retetaCurenta.id = item['id']
          retetaCurenta.retetaIngredient = []
          retetaCurenta.tipReteta= {nume_Tip_Retete:item['nume_Tip_Retete']}
          retetaCurenta.poza_reteta = item['poza_reteta']
          retetaCurenta.instructiuni=item['instructiuni_reteta']
          retetaCurenta.nume_pahar=item['nume_Pahar']
          retetaCurenta.rating=item['rating_retea']
        }

        //console.log(retetaCurenta.nume_reteta)
        retetaCurenta.retetaIngredient.push({
          nume_ingredient:item['nume_ingredient'],
          idIngredient:item['idIngredient'],
          unitate:item['nume_unitate'],
          cantitate: item['cantitate_Ingredient']

        });
      })

      return Object.values(retete);
    }

    async getRetetaRandom(){
      var reteta = (await this.http.get(environment.baseUrl+"Retete/random").toPromise() as Array<any>);
      //console.log(reteta , "reteta")
      return this.groupBy(reteta)[0] //nu merge incarcare modal
    }

    async allRetete(){
      var reteta = (await this.http.get(environment.baseUrl+"Retete/all").toPromise() as Array<any>);
     // console.log(reteta)
      return reteta
    }
    async toggleLike(model_title){

      var reteta = (await this.http.post(environment.baseUrl+"Retete/like",{Name:model_title, Token:localStorage.getItem('token')}).toPromise() as Array<any>);
     // console.log(reteta, 'toggle');
    }
    async fetchReteteApreciate(){
      var retete = [];
       var reteteliked = (await this.getLikedRetete()as Array<any>);
      //console.log("fetchReteteApreciate ",)
      reteteliked.forEach(reteta => {
          (this.getReteta(reteta.idReteta)).then(async (reteta)=>{
            reteta[0].liked = true;

            //reteta[0].user_rating = await this.ReteteService.getReviewReteta(reteta[0].nume_reteta);
            retete.push(reteta[0])
        });
      });
      return retete;
    }

    async submitReview(reteta, review){
      return (await this.http.post(environment.baseUrl+"Aprecieri/SubmitReview",{Name:reteta, Token:localStorage.getItem('token'), Review:review}).toPromise() as any);
    }
    async getRetete(initialRequest:boolean):Promise<any>{
      var result = (await this.http.post(environment.baseUrl+"Retete",{initialRequest}).toPromise() as Array<any>);

      var likedRetete = (await this.http.post(environment.baseUrl+"Aprecieri/GetByIdLikes",{token:localStorage.getItem('token')}).toPromise() as Array<any>);

      likedRetete.forEach(lR =>{
        try{
          let reteta = (result.find((x:any) =>{   return x.id == lR.idReteta;}) as any);
          if(reteta){
            reteta.liked = true;
          }
        }catch(e){}
      });


      var reviewRetete = (await this.http.post(environment.baseUrl+"Aprecieri/GetByIdReviews",{token:localStorage.getItem('token')}).toPromise() as Array<any>);

      reviewRetete.forEach(rR =>{
         try{
          let reteta = (result.find((x:any) =>{   return x.id == rR.idReteta;}) as any);
          if(reteta)
            reteta.user_rating = rR.review;
         }catch(e){}
       })

      result.forEach(r=>r.hidden = false);
      return result

    }

    async getTipuriRetete():Promise<any>{
      return this.http.get(environment.baseUrl+"TipuriRetete/").toPromise();
    }

    async getCategoriiRetete():Promise<any>{
      return this.http.get(environment.baseUrl+"CategoriiRetete/").toPromise() ;
    }


    getTipReteta(idTipReteta:string):Promise<any>{
      return this.http.get(environment.baseUrl+"TipuriRetete/"+idTipReteta).toPromise();
    }
    getIngrediente(idReteta:string):Promise<any>{
      return this.http.get(environment.baseUrl+"ReteteIngrediente/GetByReteta/"+idReteta).toPromise();
    }
    getNumeIngredient(idIngredient:string):Promise<any>{
      return this.http.get(environment.baseUrl+"Ingrediente/"+idIngredient).toPromise();
    }

    async getSubcategIngredient(idSubcateg:string):Promise<any>{
      var result = (await this.http.get(environment.baseUrl+"SubCategoriiIngrediente/"+idSubcateg).toPromise() )as Array<any> ;
      return result
    }

    getLikedRetete():Promise<any>{


      return this.http.post(environment.baseUrl+"Aprecieri/GetByIdLikes",{token:localStorage.getItem('token')}).toPromise();
    }


     async getReteta(idReteta:string):Promise<any>{
      var rezultat = (await this.http.get(environment.baseUrl+"Retete/GetById/"+idReteta).toPromise()) as Array<any>;

      var reviewRetete = (await this.http.post(environment.baseUrl+"Aprecieri/GetByIdReviews",{token:localStorage.getItem('token')}).toPromise() as Array<any>);
      reviewRetete.forEach(rR =>{
         try{
          (rezultat.find((x:any) =>{   return x.id == rR.idReteta;}) as any).user_rating = rR.review;
         }catch(e){}
       })
      return rezultat; //this.groupBy(rezultat);
    }

    async getReteteSugerate(arrayIngrediente, perfectMatch = false){
      var reteta = (await this.http.post(environment.baseUrl+"Retete/Generate",{Ingrediente:arrayIngrediente, Token:localStorage.getItem('token'), PerfectMatch: perfectMatch}).toPromise() as Array<any>);
      return reteta;
    }

    async getRecommandations(){
      var retete = (await this.http.post(environment.baseUrl+"Aprecieri/GeneratePersonalSugestions",{Token:localStorage.getItem('token')}).toPromise() as Array<any>);

      return retete;
    }

    async getRetetaZilei(){
      var reteta = (await this.http.post(environment.baseUrl+"Retete/RetetaZilei",{}).toPromise() as Array<any>);

      return reteta;
    }
    //get all ingrediente
    async getIngredienteAll():Promise<any>{
      var result = await this.http.get(environment.baseUrl+"Ingrediente/").toPromise() as Array<any> ;
      return result
    }




}

