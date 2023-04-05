import { Client } from "./apis/apiModels";

declare module "*.vue" {
    import Vue from "vue";
    export default Vue;
}

declare module "vue/types/vue" {
    interface Vue {
        $api: Client;
    }
}
