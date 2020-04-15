import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CustomersComponent } from './customers/customers/customers.component';
import { FilterTextboxComponent } from './shared/filter-textbox.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CustomersComponent,
    FilterTextboxComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'customers', component: CustomersComponent, pathMatch: 'full' },
      { path: '', pathMatch: 'full' , redirectTo: '/customers'},

    ])
  ],
  providers: [FilterTextboxComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
