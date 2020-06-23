import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthoriseComponent} from "./authorise.component";

const routes: Routes = [
  { path: '', component: AuthoriseComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthoriseRoutingModule { }
