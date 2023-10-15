import { Component, OnInit } from '@angular/core';
import { DeactiveTradingUserByUsernameRequest } from 'src/app/_models/DeactiveTradingUserByUsernameRequest';
import { TradingUserDto } from 'src/app/_models/TradingUserDto';
import { TradingUsersService } from 'src/app/_services/trading-users.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  tradingUsers: TradingUserDto[] = [];

  constructor(private tradingUserService: TradingUsersService) {
  }

  ngOnInit(): void {
    this.loadTradingUsers();
  }

  loadTradingUsers() {
    this.tradingUserService.getTradingUsers().subscribe((response) => {
      this.tradingUsers = response;
    });
  }

  deactiveTradingUser(username: string) {
    this.tradingUserService
      .deactiveTradingUserByUsername(username)
      .subscribe((user) => {
        this.loadTradingUsers();
      });
  }

  deleteTradingUser(username: string) {
    this.tradingUserService.deleteTradingUserByUsername(username);
  }
}
