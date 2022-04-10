import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FirstPagePage } from '../pages/first-page/first-page.page';
import { TabsPage } from './tabs.page';

const routes: Routes = [
  {
    path: '',
    component: TabsPage,
    children: [
      {
        path: 'tab1',
        loadChildren: () => import('../tab1/tab1.module').then(m => m.Tab1PageModule)

      },

      {
        path: 'tab1-toate-retetele',
        loadChildren: () => import('../tab1-toate-retetele/tab1-toate-retetele-routing.module').then( m => m.Tab1ToateRetetelePageRoutingModule)
      },

      {
        path: 'tab2',
        loadChildren: () => import('../tab2/tab2.module').then(m => m.Tab2PageModule)
      },
      {
        path: 'tab3',
        loadChildren: () => import('../tab3/tab3.module').then(m => m.Tab3PageModule)
      },



      {
        path: '',
        redirectTo: '/home/tab1',
        pathMatch: 'full'
      }
    ]
  },
  // {
  //   path: '',
  //   redirectTo: '/tabs/tab1',
  //   pathMatch: 'full'
  // }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
})
export class TabsPageRoutingModule {}
