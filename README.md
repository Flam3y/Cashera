# Cashera

.NET client for the [Cashera](https://cashera.cash) payment gateway API.

## Creating a payment

```csharp
var client = new CasheraClient("pk_...", "sk_...");

Transaction transaction = await client.CreateTransaction(
    amount: 49900,                 // minor units: 499.00 RUB
    currency: Currency.Rub,
    externalId: "order-10428",
    description: "Pro subscription, 1 month",
    paymentMethod: PaymentMethod.Sbp); // null = common payment form

// Send the customer to transaction.PaymentUrl
```

## Checking the status

```csharp
Transaction byUuid = await client.GetTransaction(transaction.Uuid);
Transaction byExternalId = await client.GetTransactionByExternalId("order-10428");

if (byUuid.Status == TransactionStatus.Paid) { /* ... */ }
```

API errors throw `ApiRequestException` with `ErrorCode`, `Message` and validation `Errors`.

## Webhooks

Deserialize the body into `Update` with `JsonApi.Options` and check the headers with
`client.ValidateWebhook(apiKey, secret)` (requires the secret). Always answer 2xx,
including for unknown events and `webhook.test`.
