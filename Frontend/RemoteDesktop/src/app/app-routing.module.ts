import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },
  { path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule) },
  { path: 'profile', loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule) },
  { path: 'authorise', loadChildren: () => import('./authorise/authorise.module').then(m => m.AuthoriseModule) },
  { path: '', redirectTo: 'authorise', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
  constructor() {
    if (localStorage.getItem('rdc_user') == null)
      routes.concat({ path: '', redirectTo: 'authorise', pathMatch: 'full' })
    else if (localStorage.getItem('rdc_role') == 'admin')
      routes.concat({ path: '', redirectTo: 'admin', pathMatch: 'full' })
    else
      routes.concat({ path: '', redirectTo: 'user', pathMatch: 'full' })
  }
}
