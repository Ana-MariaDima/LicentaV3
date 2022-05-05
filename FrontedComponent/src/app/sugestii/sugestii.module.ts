import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { SugestiiPageRoutingModule } from './sugestii-routing.module';

import { SugestiiPage } from './sugestii.page';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    SugestiiPageRoutingModule,
    Ng2SearchPipeModule
  ],
  declarations: [SugestiiPage]
})
export class SugestiiPageModule {}
