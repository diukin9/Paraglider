import { ReactNode } from "react";

import { AuthProvider } from "../../../../typings/server";
import VkIcon from "./images/vk.svg";
import YandexIcon from "./images/yandex.svg";

export const getAuthProviderIconLink = (authProvider: AuthProvider): string | undefined => {
  switch (authProvider) {
    case AuthProvider.Yandex:
      return YandexIcon;

    case AuthProvider.Vkontakte:
      return VkIcon;

    default:
      return undefined;
  }
};

export const getAuthProviderName = (authProvider: AuthProvider): ReactNode => {
  switch (authProvider) {
    case AuthProvider.Yandex:
      return "Яндекс ID";

    case AuthProvider.Vkontakte:
      return "Вконтакте";

    default:
      return null;
  }
};
