import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoryService } from './services/Ingrediente/category.service';
import { ReteteService } from './services/Retete/retete.service';
import { IonicRatingModule } from 'ionic4-rating';
import { Tab1ToateRetetelePageModule } from './tab1-toate-retetele/tab1-toate-retetele.module';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [BrowserModule, IonicModule.forRoot({animated: false}), AppRoutingModule, ReactiveFormsModule, HttpClientModule,BrowserModule,FormsModule, ReactiveFormsModule,IonicRatingModule, Tab1ToateRetetelePageModule, Ng2SearchPipeModule],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }, CategoryService,ReteteService],

  bootstrap: [AppComponent],
})
export class AppModule {}
