import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilterTextboxComponent } from './filter-textbox.component';
import { PropertyResolver } from './property-resolver';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationComponent } from './pagination/pagination.component';



@NgModule({
  declarations: [PropertyResolver, PaginationComponent],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  exports: [PaginationComponent, PropertyResolver]
})
export class SharedModule { }
