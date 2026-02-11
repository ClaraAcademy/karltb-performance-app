export const LoginFieldType = {
  Username: "username",
  Password: "password",
} as const;

export type LoginFieldType =
  (typeof LoginFieldType)[keyof typeof LoginFieldType];
