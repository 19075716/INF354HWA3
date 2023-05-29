import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { SidebarComponent } from './common/sidebar/sidebar.component';
import { RegisterComponent } from './register/register.component';
import { ProductListingComponent } from './product-listing/product-listing.component';

const routes: Routes = [
{path: '', component: LoginPageComponent},
{path: 'login-page', component: LoginPageComponent},
{path: 'sidebar', component: SidebarComponent},
{path: 'register', component: RegisterComponent},
{path: 'products', component: ProductListingComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
