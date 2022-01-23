import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Paths } from './constants/paths';
import { AuthorizeGuard } from './guards/authorize.guard';
import { HomePageGuard } from './guards/home-page.guard';
import { BoardComponent } from './pages/board/board.component';
import { HomeComponent } from './pages/home/home.component';
import { WelcomeComponent } from './pages/welcome/welcome.component';

export const routes: Routes = [
  { path: Paths.home, component: HomeComponent, canActivate: [HomePageGuard], pathMatch: 'full' },
  { path: Paths.welcome, component: WelcomeComponent, canActivate: [AuthorizeGuard] },
  { path: `${Paths.board}/:id`, component: BoardComponent, canActivate: [AuthorizeGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
