import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { SugestiiPageRoutingModule } from './sugestii-routing.module';

import { SugestiiPage } from './sugestii.page';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { StarsComponentModule } from '../stars/stars.module';



@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    SugestiiPageRoutingModule,
    Ng2SearchPipeModule,
    StarsComponentModule,

  ],
  declarations: [SugestiiPage]
})
export class SugestiiPageModule {}
