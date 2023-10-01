import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TradingUserDto } from 'src/app/_models/TradingUserDto';
import { TradingUsersService } from 'src/app/_services/trading-users.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit {
  tradingUser: TradingUserDto | null = null;
  constructor(
    private tradingUserService: TradingUsersService,
    private route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.loadTradingUser();
  }
  loadTradingUser() {
    this.tradingUserService
      .getTradingUserByUsername(this.route.snapshot.paramMap.get('username'))
      .subscribe((user) => {
        this.tradingUser = user;
      });
  }
}
