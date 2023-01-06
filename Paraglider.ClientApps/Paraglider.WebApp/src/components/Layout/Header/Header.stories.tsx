import { Meta, StoryObj } from "@storybook/react";

import { Header } from ".";

const meta: Meta<typeof Header> = {
  component: Header,
};

export const Default: StoryObj<typeof Header> = {};
Default.storyName = "Header";

export default meta;
