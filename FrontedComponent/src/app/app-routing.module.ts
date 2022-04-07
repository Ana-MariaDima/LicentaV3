import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { LoggedInGuard } from './logged-in.guard';
import { LoggedOutGuard } from './logged-out.guard';
import { TabsPageModule } from './tabs/tabs.module';
import { TabsPage } from './tabs/tabs.page';

const routes: Routes = [
  {
    path: '',
    canActivate:[LoggedOutGuard],
    loadChildren: () => import('./pages/welcome/welcome.module').then( m => m.WelcomePageModule)
  },
  {
    path: 'login',
    canActivate:[LoggedOutGuard],
    loadChildren: () => import('./pages/login/login.module').then( m => m.LoginPageModule)
  },
  {
    path: 'home',
    canActivate:[LoggedInGuard],
    loadChildren: () => import('./tabs/tabs.module').then(m => m.TabsPageModule)
  },
  {
    path: 'register',
    canActivate:[LoggedOutGuard],
    loadChildren: () => import('./pages/singup/singup.module').then(m => m.SingupPageModule)
  },
  {
    path: 'modal-popup-ing',
    loadChildren: () => import('./modal-popup-ing/modal-popup.module').then( m => m.ModalPopupPageModule)
  },
  {
    path: 'modal-popup-ret',
    loadChildren: () => import('./modal-popup-ret/modal-popup-ret.module').then( m => m.ModalPopupPageRetModule)
  },



  {
    path: 'tab1-toate-retetele',
    loadChildren: () => import('./tab1-toate-retetele/tab1-toate-retetele.module').then( m => m.Tab1ToateRetetelePageModule)
  }



];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
