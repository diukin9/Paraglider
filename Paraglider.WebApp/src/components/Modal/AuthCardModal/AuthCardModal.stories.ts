import { Meta, StoryObj } from "@storybook/react";

import { AuthCardModal } from "./AuthCardModal";

const meta: Meta<typeof AuthCardModal> = {
  component: AuthCardModal,
};

export const Default: StoryObj<typeof AuthCardModal> = {};
Default.storyName = "AuthCardModal";

export default meta;
