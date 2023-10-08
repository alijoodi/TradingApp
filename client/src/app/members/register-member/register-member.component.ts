import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RegisterTradingUserDto } from 'src/app/_models/RegisterTradingUserDto';
import { TradingUserDto } from 'src/app/_models/TradingUserDto';
import { TradingUsersService } from 'src/app/_services/trading-users.service';

@Component({
  selector: 'app-register-member',
  templateUrl: './register-member.component.html',
  styleUrls: ['./register-member.component.css'],
})
export class RegisterMemberComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  tradingUser: TradingUserDto | null = null;
  registerTradingUser: RegisterTradingUserDto = {} as RegisterTradingUserDto;

  constructor(
    private tradingUserService: TradingUsersService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  registerTradingUserMethod() {
    console.log(this.registerTradingUser);

    this.tradingUserService
      .registerTradingUser(this.registerTradingUser)
      .subscribe(
        (user) => {
          this.tradingUser = user;
          console.log(user);
          this.toastr.success('user has been created');
          this.router.navigateByUrl('/members');
        },
        (error) => {
          this.toastr.error(error);
        }
      );
  }
}
