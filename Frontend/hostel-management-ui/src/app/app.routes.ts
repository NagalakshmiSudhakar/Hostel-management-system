
import { Routes } from '@angular/router';

import { AdminLayoutComponent } from './features/admin/layout/admin-layout/admin-layout';
import { StudentsListComponent } from './features/students/pages/students-list/students-list';
import { Dashboard } from './features/admin/pages/dashboard/dashboard';
import { StudentCreateComponent } from './features/students/pages/student-create/student-create';
import { StudentEditComponent } from './features/students/pages/student-edit/student-edit';

import { RoomsListComponent } from './features/rooms/pages/rooms-list/rooms-list';
import { RoomCreate } from './features/rooms/pages/room-create/room-create';
import { RoomEdit } from './features/rooms/pages/room-edit/room-edit';
import { Login } from './features/auth/pages/login/login';
import { authGuard } from './core/guards/auth-guard';
export const routes: Routes = [
  {
    path: 'login',
    component: Login
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    canActivate: [authGuard],
    children: [
      {
        path: 'dashboard',
        component: Dashboard
      },
      {
        path: 'students/create',
        component: StudentCreateComponent
      },
      {
        path: 'students/edit/:id',
        component: StudentEditComponent
      },
      {
        path: 'students',
        component: StudentsListComponent,
        pathMatch: 'full'
      },
      {
  path: 'rooms/create',
  component: RoomCreate
},
      {
        path: 'rooms',
        component: RoomsListComponent,
        pathMatch: 'full'
      },
      { path: 'rooms/edit/:id', component: RoomEdit },
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
