import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { Tab1ToateRetetelePageRoutingModule } from './tab1-toate-retetele-routing.module';

import { Tab1ToateRetetelePage } from './tab1-toate-retetele.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    Tab1ToateRetetelePageRoutingModule
  ],
  declarations: [Tab1ToateRetetelePage]
})
export class Tab1ToateRetetelePageModule {}
