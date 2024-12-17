import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './features/product/components/product-list/product-list.component';

//import { ProductDetailComponent } from './features/product/product-detail/product-detail.component';

const routes: Routes = [
  { path: 'products', component: ProductListComponent },
  { path: '', redirectTo: '/products', pathMatch: 'full' },
];


//const routes: Routes = [
//  { path: '', redirectTo: '/products', pathMatch: 'full' },
//  { path: 'products', component: ProductListComponent },
// // { path: 'products/:id', component: ProductDetailComponent },
//];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
