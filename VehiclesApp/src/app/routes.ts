import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MakesComponent } from './makes/makes-list/makes.component';
import { ModelsComponent } from './models/models-list/models.component';
import { MakeEditComponent } from './makes/make-edit/make-edit.component';
import { MakeCreateComponent } from './makes/make-create/make-create.component';
import { MakeDeleteComponent } from './makes/make-delete/make-delete.component';
import { ModelCreateComponent } from './models/model-create/model-create.component';
import { ModelEditComponent } from './models/model-edit/model-edit.component';
import { ModelDeleteComponent } from './models/model-delete/model-delete.component';
import { MakesListResolver } from './_resolvers/makes-list.resolver';
import { MakeResolver } from './_resolvers/make.resolver';
import { ModelsListResolver } from './_resolvers/models-list.resolver';
import { ModelResolver } from './_resolvers/model.resolver';

export const appRoutes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'makes', component: MakesComponent, resolve: {makes: MakesListResolver} },
  { path: 'makes/create', component: MakeCreateComponent },
  { path: 'makes/:id/edit', component: MakeEditComponent, resolve: {make: MakeResolver} },
  { path: 'makes/:id/delete', component: MakeDeleteComponent, resolve: {make: MakeResolver} },
  { path: 'makes/:id/models', component: ModelsComponent, resolve: {models: ModelsListResolver} },
  { path: 'makes/:id/models/create', component: ModelCreateComponent },
  { path: 'makes/:id/models/:modelId/edit', component: ModelEditComponent, resolve: {model: ModelResolver} },
  { path: 'makes/:id/models/:modelId/delete', component: ModelDeleteComponent, resolve: {model: ModelResolver} },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
