import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BoardComponent } from './board/board.component';
import { Paths } from './constants/paths';
import { AuthorizeGuard } from './guards/authorize.guard';
import { HomePageGuard } from './guards/home-page.guard';
import { HomeComponent } from './home/home.component';
import { WelcomeComponent } from './welcome/welcome.component';

export const routes: Routes = [
  { path: Paths.home, component: HomeComponent, canActivate: [HomePageGuard], pathMatch: 'full' },
  { path: `${Paths.board}/:id`, component: BoardComponent, canActivate: [AuthorizeGuard] },
  { path: Paths.welcome, component: WelcomeComponent, canActivate: [AuthorizeGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
