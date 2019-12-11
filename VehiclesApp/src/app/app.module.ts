import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { MakeService } from './_services/make.service';
import { MakesComponent } from './makes/makes-list/makes.component';
import { ModelsComponent } from './models/models-list/models.component';
import { HomeComponent } from './home/home.component';
import { appRoutes } from './routes';
import { MakeEditComponent } from './makes/make-edit/make-edit.component';
import { MakeCreateComponent } from './makes/make-create/make-create.component';
import { MakeDeleteComponent } from './makes/make-delete/make-delete.component';
import { ModelCreateComponent } from './models/model-create/model-create.component';
import { ModelEditComponent } from './models/model-edit/model-edit.component';
import { ModelDeleteComponent } from './models/model-delete/model-delete.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      MakesComponent,
      ModelsComponent,
      MakeEditComponent,
      MakeCreateComponent,
      MakeDeleteComponent,
      ModelCreateComponent,
      ModelEditComponent,
      ModelDeleteComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(appRoutes, {onSameUrlNavigation: 'reload'})
   ],
   providers: [
      MakeService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
