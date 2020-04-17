import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CustomersComponent } from './customers/customers.component';
import { FilterTextboxComponent } from './shared/filter-textbox.component';
import { CustomersGridComponent } from './customers/customers-grid/customers-grid.component';
import { CustomersEditComponent } from './customers/customers-edit/customers-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CustomersComponent,
    FilterTextboxComponent,
    CustomersGridComponent,
    CustomersEditComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'customers', component: CustomersComponent, pathMatch: 'full' },
      { path: 'customers/:id', component: CustomersEditComponent, pathMatch: 'full' },
      { path: '', pathMatch: 'full' , redirectTo: '/customers'},

    ])
  ],
  providers: [FilterTextboxComponent, CustomersComponent, CustomersGridComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
