import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { DeactiveTradingUserByUsernameRequest } from 'src/app/_models/DeactiveTradingUserByUsernameRequest';
import { TradingUserDto } from 'src/app/_models/TradingUserDto';
import { PagenatedResult, Pagenation } from 'src/app/_modules/pagenation';
import { TradingUsersService } from 'src/app/_services/trading-users.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  showDirectionLinks = true;
  pagenatedResult: PagenatedResult<TradingUserDto[]> = new PagenatedResult<TradingUserDto[]>();
  pagenation: Pagenation;
  pageNumber = 1;
  pageSize = 10;

  tradingUsers: TradingUserDto[] = [];

  constructor(private tradingUserService: TradingUsersService) {
  }

  ngOnInit(): void {
    this.loadTradingUsers(this.pageNumber, this.pageSize);
  }

  loadTradingUsers(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page && itemsPerPage) {
      params = params.append("pageNumber", page.toString());
      params = params.append("pageSize", itemsPerPage.toString());
    }

    this.tradingUserService.getTradingUsers(params).pipe(map((response) => {
      this.pagenatedResult.result = response.body;
      if (response.headers.get("Pagenation") !== null) {
        this.pagenatedResult.pagenation = JSON.parse(response.headers.get("Pagenation"));
      }
    })).subscribe(response => {
      this.tradingUsers = this.pagenatedResult.result;
      this.pagenation = this.pagenatedResult.pagenation;
    });
  }

  deactiveTradingUser(username: string) {
    this.tradingUserService
      .deactiveTradingUserByUsername(username)
      .subscribe((user) => {
        this.loadTradingUsers(this.pageNumber, this.pageSize);
      });
  }

  deleteTradingUser(username: string) {
    this.tradingUserService.deleteTradingUserByUsername(username).subscribe(() => {
      this.loadTradingUsers(this.pageNumber, this.pageSize);
    });
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadTradingUsers(this.pageNumber, this.pageSize);

  }
}
