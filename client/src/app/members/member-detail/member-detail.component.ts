import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TradingUserDto } from 'src/app/_models/TradingUserDto';
import { TradingUsersService } from 'src/app/_services/trading-users.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  tradingUser: TradingUserDto | null = null;
  constructor(
    private tradingUserService: TradingUsersService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.loadTradingUser(this.route.snapshot.paramMap.get('username'));
    console.log(this.route.snapshot.paramMap.get('username'));
  }
  ngOnInit(): void {}
  loadTradingUser(username: string) {
    this.tradingUserService
      .getTradingUserByUsername(username)
      .subscribe((user) => {
        this.tradingUser = user;
      });
  }

  updateTradingUser() {
    this.tradingUserService.updateTradingUser(this.tradingUser).subscribe(
      (user) => {
        this.tradingUser = user;
        this.toastr.success('user has been updated');
        this.router.navigateByUrl('/members');
      },
      (error) => {
        this.toastr.error(error);
      }
    );
  }

  deactiveTradingUser(username: string) {
    this.tradingUserService
      .deactiveTradingUserByUsername(username)
      .subscribe((user) => {
        this.tradingUser = user;
      });
  }
}
