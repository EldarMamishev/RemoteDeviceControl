import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { filter, map, shareReplay } from 'rxjs/operators';
import { BkLayout } from '@uibakery/kit';
import { LIGHT_THEME, DARK_THEME } from './theme';
import {AppModule} from "./app.module";
import {NbThemeModule, NbThemeService} from "@nebular/theme";
import {TranslateService} from "@ngx-translate/core";
import {environment} from "../environments/environment";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
  private theme = 'light';
  private language = 'en';

  defaultLayout: BkLayout = {
    paddings: {
      paddingTop: 16,
      paddingRight: 16,
      paddingBottom: 16,
      paddingLeft: 16,
      paddingTopUnit: "px",
      paddingRightUnit: "px",
      paddingBottomUnit: "px",
      paddingLeftUnit: "px"
    },
    header: true,
    sidebar: true
  };

  layout$: Observable<BkLayout> = this.router.events
    .pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => {
        let route = this.router.routerState.root;
        while (route.firstChild) {
          route = route.firstChild;
        }
        return route.snapshot.data['layout'] || this.defaultLayout;
      }),
      shareReplay(),
    );

  padding$: Observable<string> = this.layout$
    .pipe(
      map((layout: BkLayout) => this.getPaddingCssValue(layout.paddings)),
    );
  items =   [
    {
      "title": "Admin",
      "link": "/admin",
      "children": null
    },
    {
      "title": "User",
      "link": "/user",
      "children": null
    },
    {
      "title": "Authorise",
      "link": "/authorise",
      "children": null
    },
    {
      "title": "Profile",
      "link": "/profile",
      "children": null
    }
  ];

  constructor(private router: Router, private themeService: NbThemeService, private translate: TranslateService) {
   }

  ngOnInit(): void {
    this.translate.use(environment.defaultLocale);
  }

  public OnSelectedThemeChanged() {
    this.themeService.changeTheme(this.theme);

    this.themeService.onThemeChange()
      .subscribe((theme: any) => {
        console.log(`Theme changed to ${theme.name}`);
      });
  }

  public OnSelectedLanguageChanged() {
    this.translate.use(this.language);
  }

  private OnProfilePhotoClicked() {
    this.router.navigate(['./profile']);
  }

  private OnHomeClicked() {
    this.router.navigate(['.']);
  }

  private getPaddingCssValue(paddings): string {
    return `${paddings.paddingTop}${paddings.paddingTopUnit} ` +
           `${paddings.paddingRight}${paddings.paddingRightUnit} ` +
           `${paddings.paddingBottom}${paddings.paddingBottomUnit} ` +
           `${paddings.paddingLeft}${paddings.paddingLeftUnit}`;
  }
}
