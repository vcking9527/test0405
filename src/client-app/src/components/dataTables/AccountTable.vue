<template>
  <div>
    <div class="d-flex">
      <v-spacer></v-spacer>
      <v-btn class="primary" @click="onRefresh">重新整理</v-btn>
    </div>
    <v-card class="mt-2 mb-2" elevation="0" outlined>
      <v-data-table
        :headers="headers"
        :items="items"
        :options.sync="syncPageOptions"
        :server-items-length="syncPageOptions.totalItems"
        loading-text="讀取中請稍後"
        :loading="loading"
        :footer-props="{
          itemsPerPageOptions: [5, 10, 15],
          showFirstLastPage: true,
        }"
        disable-filtering
      >
        <template #no-data>
          <span class="primary--text subtitle-1">沒有資料</span>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script lang="ts">
import {
  Component,
  Vue,
  Prop,
  PropSync,
  Emit,
  Watch,
} from "vue-property-decorator";
import { IAccountDto } from "@/apis/apiModels";
import { IPageOptions } from "@/models/page";

@Component({})
export default class AccountTable extends Vue {
  private headers = [
    {
      text: "使用者編號",
      align: "center",
      value: "accountNumber",
      sortable: true,
    },
    { text: "名稱", align: "center", value: "name", sortable: true },
    { text: "信箱", align: "center", value: "email", sortable: true },
    { text: "餘額", align: "center", value: "balance", sortable: true },
    { text: "年齡", align: "center", value: "age", sortable: true },
    { text: "性別", align: "center", value: "gender", sortable: true },
  ];

  @Prop({ type: Array, default: () => [] })
  items!: IAccountDto[];

  @Prop({ type: Boolean, default: () => false })
  loading!: boolean;

  @PropSync("pageOptions", {
    type: Object,
    default: () => ({
      page: 1,
      itemsPerPage: 5,
      sortBy: [],
      sortDesc: [],
      groupBy: [],
      groupDesc: [],
      multiSort: false,
      mustSort: false,
    }),
  })
  syncPageOptions!: IPageOptions;

  @Watch("syncPageOptions", { immediate: true, deep: true })
  onSyncPageOptionsChange(
    newPageOptions: IPageOptions,
    oldPageOptions: IPageOptions
  ) {
    // 頁面相同、第一次設定頁面(忽略)
    console.log(newPageOptions, oldPageOptions);

    if (
      !oldPageOptions ||
      (newPageOptions.page === oldPageOptions.page &&
        newPageOptions.itemsPerPage === oldPageOptions.itemsPerPage)
    ) {
      return;
    }

    this.onPageOptionsChange(newPageOptions);
  }

  @Emit("pageOptionsChange")
  onPageOptionsChange(options: IPageOptions) {}

  @Emit("refresh")
  onRefresh() {}
}
</script>

 
