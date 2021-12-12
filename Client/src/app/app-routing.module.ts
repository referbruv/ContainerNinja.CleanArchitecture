import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddoreditComponent } from './components/pages/addoredit/addoredit.component';
import { HomeComponent } from './components/pages/home/home.component';
import { ItemsListComponent } from './components/pages/items-list/items-list.component';
import { LoginComponent } from './components/pages/login/login.component';
import { AuthGuard } from './providers/auth.guard';

const routes: Routes = [
  {
    path: 'items', canActivate: [AuthGuard], children: [
      { path: '', component: ItemsListComponent },
      { path: 'add', component: AddoreditComponent },
      { path: ':id', component: AddoreditComponent },
      { path: ':id/edit', component: AddoreditComponent }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: '', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
