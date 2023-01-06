import { Meta, StoryObj } from "@storybook/react";

import { BackButton } from ".";

const meta: Meta<typeof BackButton> = {
  component: BackButton,
};

export const Default: StoryObj<typeof BackButton> = {};
Default.storyName = "BackButton";

export default meta;
