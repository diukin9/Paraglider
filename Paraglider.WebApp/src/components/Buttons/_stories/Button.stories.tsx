import React from "react";
import { Meta } from "@storybook/react";
import { Button } from "../Button";

export const Default = () => <Button>Текст кнопки</Button>;

export default {
  title: "Components/Buttons/Button",
  component: Button,
} as Meta;
