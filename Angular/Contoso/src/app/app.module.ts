import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { GridModule } from '@progress/kendo-angular-grid';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { DropDownsModule, DropDownListModule } from '@progress/kendo-angular-dropdowns';
import { DatePickerModule } from '@progress/kendo-angular-dateinputs';

import { AppComponent } from './app.component';
import { GenericCreateComponent } from './generic/generic-create/generic-create.component';
import { GenericDeleteComponent } from './generic/generic-delete/generic-delete.component';
import { GenericDetailComponent } from './generic/generic-detail/generic-detail.component';
import { GenericEditComponent } from './generic/generic-edit/generic-edit.component';
import { GenericGridComponent } from './generic/generic-grid/generic-grid.component';
import { FormFieldDropdownComponent } from './generic/form-field-dropdown/form-field-dropdown.component';
import { FormFieldMultiselectComponent } from './generic/form-field-multiselect/form-field-multiselect.component';
import { GridColumnDropdownFilterComponent } from './generic/grid-column-dropdown-filter/grid-column-dropdown-filter.component';
import { GridColumnMultiselectFilterComponent } from './generic/grid-column-multiselect-filter/grid-column-multiselect-filter.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ScreenHostComponent } from './screen-host/screen-host.component';
import { HtmlPageComponent } from './html-page/html-page.component';
import { DisplayDropdownValueComponent } from './generic/display-dropdown-value/display-dropdown-value.component';
import { AboutComponent } from './about/about.component';



@NgModule({
  declarations: [
    AppComponent,
    GenericCreateComponent,
    GenericDeleteComponent,
    GenericDetailComponent,
    GenericEditComponent,
    GenericGridComponent,
    FormFieldDropdownComponent,
    FormFieldMultiselectComponent,
    GridColumnDropdownFilterComponent,
    GridColumnMultiselectFilterComponent,
    NavBarComponent,
    ScreenHostComponent,
    HtmlPageComponent,
    DisplayDropdownValueComponent,
    AboutComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ButtonsModule,
    DropDownsModule,
    DropDownListModule,
    DatePickerModule,
    GridModule,
    NgbModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: 'baseUrl', useValue: 'http://localhost:57303' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
