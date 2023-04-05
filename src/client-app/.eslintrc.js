module.exports = {
    root: true,
    env: {
        node: true,
    },
    extends: [
        "plugin:vue/essential",
        "eslint:recommended",
        "@vue/typescript/recommended",
    ],
    parserOptions: {
        ecmaVersion: 2020,
    },
    rules: {
        "no-console": process.env.NODE_ENV === "production" ? "warn" : "off",
        "no-debugger": process.env.NODE_ENV === "production" ? "warn" : "off",
        "no-unused-vars": "off",
        "@typescript-eslint/no-unused-vars": ["off"],
        "camelcase": "off",
        '@typescript-eslint/camelcase': ['off'],
        "import/no-mutable-exports": "off",
        "no-use-before-define": "off",
        "@typescript-eslint/no-use-before-define": ["error"],         
        // 防止apiModels註解的問題
        'spaced-comment': ["error", "always", { "exceptions": ["-"] }],
        'lines-between-class-members': ["error", "always", { 'exceptAfterSingleLine': true }],
        // 隱藏warning  Insert `;`  prettier/prettier
        'prettier/prettier': ['off', { endOfLine: 'auto' },],
        // 隱藏error    Empty block statement  no-empty
        "no-empty": ["error", { "allowEmptyCatch": true }],
        "@typescript-eslint/no-empty-function": "off"
    },
};
