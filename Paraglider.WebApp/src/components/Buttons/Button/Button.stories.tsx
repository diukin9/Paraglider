import { Meta, StoryObj } from "@storybook/react";

import { Button } from ".";

const meta: Meta<typeof Button> = {
  component: Button,
  args: {
    variant: "default",
  },
  argTypes: {
    variant: {
      options: ["default", "outlined", "ghost"],
      control: "radio",
    },
  },
};

export const Default: StoryObj<typeof Button> = {
  args: {
    children: "Нажать",
  },
};
Default.storyName = "Button";

export default meta;
