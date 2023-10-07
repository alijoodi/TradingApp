import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TradingUserDto } from 'src/app/_models/TradingUserDto';
import { TradingUsersService } from 'src/app/_services/trading-users.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'],
})
export class MemberEditComponent implements OnInit {
  tradingUser: TradingUserDto | null;

  constructor(
    private tradingUserService: TradingUsersService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadTradingUser(this.route.snapshot.paramMap.get('username'));
  }

  loadTradingUser(username: string) {
    this.tradingUserService
      .getTradingUserByUsername(username)
      .subscribe((user) => {
        this.tradingUser = user;
      });
  }

}
