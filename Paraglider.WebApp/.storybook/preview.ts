import { ThemeDecorator } from "./decorators";

export const parameters = {
  actions: { argTypesRegex: "^on[A-Z].*" },
  background: {
    default: "light",
  },
};

export const decorators = [ThemeDecorator];
