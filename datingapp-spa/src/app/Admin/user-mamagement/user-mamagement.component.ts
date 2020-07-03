import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { User } from 'src/app/_models/user';

import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { BsModalService ,BsModalRef} from 'ngx-bootstrap/modal';



@Component({
  selector: 'app-user-mamagement',
  templateUrl: './user-mamagement.component.html',
  styleUrls: ['./user-mamagement.component.css']
})
export class UserMamagementComponent implements OnInit {
users : User[];
bsModalRef  : BsModalRef;
dtOptions: DataTables.Settings = {};

  constructor(private adminService : AdminService, private modalService: BsModalService, private alertify : AlertifyService) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 2
    };
    this.getUsersWithRoles();
  }
  ngOnDestroy() {
    // Do not forget to unsubscribe the event
   
  }

  getUsersWithRoles (){
    this.adminService.getUsersWithRoles().subscribe((users :User[]) =>{
        this.users =users ;
    }, error => {
      this.alertify.error(error);
    } )
  }

  editRolesModal(user :User){
    const initialState = {
     user,
     roles :this.getRolesArray(user)
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, {initialState});
    this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate={
        roleNames : [...values.filter(el => el.checked === true).map(el =>el.name)]
      };
      if (rolesToUpdate){
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames];

        }, error => {
          this.alertify.error(error);
        })
      }
    });
  }

  private getRolesArray(user){
    const roles = [];
    const userRoles = user.roles;
    const availableRoles : any [] = [
      {name: 'Admin', value:'Admin'},
      {name: 'Moderator', value:'Moderator'},
      {name: 'Member', value:'Member'},
      {name: 'VIP', value:'Vip'},
    ];

    for ( let i=0; i<availableRoles.length; i++ ){
        let isMatch = false;
        for( let j =0 ; j < userRoles.length; j++ ){
          if(availableRoles[i].name=== userRoles[j])
          {
            isMatch=true;
            availableRoles[i].checked =true;
            roles.push(availableRoles[i]);
            break;
          }
        }
        if (!isMatch){
          availableRoles[i].checked = false;
          roles.push(availableRoles[i]);
        }

    }
    return roles;
  }

}

