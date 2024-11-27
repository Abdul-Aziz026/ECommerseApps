import { APP_INITIALIZER, ApplicationConfig, inject, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './core/interceptors/error.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import { InitService } from './core/services/init.service';
import { lastValueFrom } from 'rxjs';

// function initializeApp(initService: InitService) {
//   return () => lastValueFrom(initService.init()).finally(() => {
//     const splash = document.getElementById('initial-splash');
//     if (splash) {
//       splash.remove();
//     }
//   })
// }



export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([errorInterceptor, loadingInterceptor])),
    provideAppInitializer(() => inject(InitService).new())
    // {
    //   provide: APP_INITIALIZER, // Custom provider for InitService initialization
    //   useFactory: async (initService: InitService) => {
    //   console.log("Factory function triggered");
    //   try {
    //     await lastValueFrom(initService.init());
    //     console.log("App initialized");
    //   }
    //   catch (error) {
    //     console.error("Error in initialization:", error);
    //   }

    //     const splash = document.getElementById('initial-splash');
    //     if (splash) {
    //       splash.remove();
    //     }
    //   },
    //   deps: [InitService], // Inject InitService as a dependency
    // },
  ]
};
