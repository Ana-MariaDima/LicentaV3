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
    path: 'modal-popup-ing-all',
    loadChildren: () => import('./modal-popup-ing-toate/modal-popup-all.module').then( m => m.ModalPopupIngAllModule)
  },
  {
    path: 'modal-popup-ret',
    loadChildren: () => import('./modal-popup-ret/modal-popup-ret.module').then( m => m.ModalPopupPageRetModule)
  },
  {
    path: 'modal-popup-cart',
    loadChildren: () => import('./modal-popup-cart/modal-popup-cart.module').then( m => m.ModalPopupCartPageModule)
  },
  {
    path: 'tab-user',
    loadChildren: () => import('./tab-user/tab-user.module').then( m => m.TabUserPageModule)
  },
  {
    path: 'generate',
    loadChildren: () => import('./sugestii/sugestii.module').then( m => m.SugestiiPageModule)
  },
  {
    path: 'parola',
    loadChildren: () => import('./modal-popup-parola/modal-popup-parola.module').then (m=> m.ModalPopupParolaPageModule )
  }








];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
