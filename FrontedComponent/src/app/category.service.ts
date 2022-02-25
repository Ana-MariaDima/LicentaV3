import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }

  async getCategoriiIngrediente():Promise<any>{
    var categorii = await this.http.get(environment.baseUrl+"CategoriiIngrediente").toPromise();
    for(let index in categorii){
       var categorie = categorii[index];
       categorie.subCategoriiIngrediente = await this.getSubCategoriiIngrediente(categorie.id);
    }
    return categorii;
  }
  ///api/SubCategoriiIngrediente/GetByCategorieIngrediente/{id}

  getSubCategoriiIngrediente(idCategorieIngredient:string):Promise<any>{
    return this.http.get(environment.baseUrl+"SubCategoriiIngrediente/GetByCategorieIngrediente/"+idCategorieIngredient).toPromise();
  }
}
