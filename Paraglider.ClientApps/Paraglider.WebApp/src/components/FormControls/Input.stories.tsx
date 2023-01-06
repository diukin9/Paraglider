import { Meta, StoryObj } from "@storybook/react";

import { ControlsGroup, Input, Label } from "./Common.styles";

const meta: Meta<typeof Input> = {
  component: Input,
  args: {
    width: "400px",
  },
};

type Story = StoryObj<typeof Input>;

export const Default: Story = {};

export const WithLabel: Story = {
  render: (args) => (
    <ControlsGroup>
      <Label>Email</Label>
      <Input {...args} />
    </ControlsGroup>
  ),
};

export default meta;
