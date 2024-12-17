import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductComponent } from './features/product/components/product/product.component';
import { ProductListComponent } from './features/product/components/product-list/product-list.component';

//import { ProductService } from './services/product.service';  // Import the service
@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    ProductListComponent
  ],
  imports: [
    BrowserModule,
    ProductListComponent,  // Import the standalone component
    //HttpClientModule,  // Import HttpClientModule for HTTP requests
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

