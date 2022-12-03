import { Meta, StoryFn } from "@storybook/react";
import { Button as DefaultButton } from "../Button";

export const Button: StoryFn = ({ variant }) => (
  <DefaultButton variant={variant}>Текст кнопки</DefaultButton>
);
Button.args = {
  variant: {
    defaultValue: "default",
    options: ["default", "outlined"],
    control: { type: "radio" },
  },
};

export default {
  title: "Components/Buttons",
  component: DefaultButton,
} as Meta;
