import { Routes } from '@angular/router';

import { AdminLayoutComponent } from './features/admin/layout/admin-layout/admin-layout';
import { StudentsListComponent } from './features/students/pages/students-list/students-list';
import { Dashboard } from './features/admin/pages/dashboard/dashboard';

export const routes: Routes = [
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      {
  path: 'dashboard',
  component: Dashboard
},
      {
        path: 'students',
        component: StudentsListComponent
      },
      {
        path: '',
        redirectTo: 'students',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '',
    redirectTo: 'admin',
    pathMatch: 'full'
  }
];