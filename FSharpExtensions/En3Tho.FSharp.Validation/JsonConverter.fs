namespace En3Tho.FSharp.Validation.Json

open System
open System.Text.Json
open System.Text.Json.Serialization
open En3Tho.FSharp.Validation
open En3Tho.FSharp.Validation.CommonValidatedTypes

type NewCtorValidatorValidatedJsonConverter<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>>() =
    inherit JsonConverter<NewCtorValidatorValidated<'value, 'validator>>()

    override this.Write(writer, value, options) =
        JsonSerializer.Serialize(writer, value.Value, options)

    override this.Read(reader, typeToConvert, options) =
        let value = JsonSerializer.Deserialize<'value>(&reader, options)
        NewCtorValidatorValidated<'value, 'validator>.Make(value)

type NewCtorValidatorValidatedJsonConverterFactory() =
    inherit JsonConverterFactory()

    override this.CreateConverter(typeToConvert, options) =
        let typeArguments = typeToConvert.GetGenericArguments()
        let converterType = typedefof<NewCtorValidatorValidatedJsonConverter<_, PlainValue.Validator<_>>>.MakeGenericType(typeArguments)
        Activator.CreateInstance(converterType) :?> JsonConverter

    override this.CanConvert(typeToConvert) =
        typeToConvert.IsGenericType
        && (let typeToConvert = if typeToConvert.IsGenericTypeDefinition then typeToConvert else typeToConvert.GetGenericTypeDefinition()
            typeToConvert.Equals(typedefof<PlainValue<_>>))

type WriteOnlyNewCtorAsyncValidatorValidatedJsonConverter<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>>() =
    inherit JsonConverter<NewCtorAsyncValidatorValidated<'value, 'validator>>()

    override this.Write(writer, value, options) =
        JsonSerializer.Serialize(writer, value.Value, options)

    override this.Read(reader, typeToConvert, options) =
        invalidOp "NewCtorAsyncValidatorValidated value cannot be read from Json. Not supported."

type WriteOnlyNewCtorAsyncValidatorValidatedJsonConverterFactory() =
    inherit JsonConverterFactory()

    override this.CreateConverter(typeToConvert, options) =
        let typeArguments = typeToConvert.GetGenericArguments()
        let converterType = typedefof<WriteOnlyNewCtorAsyncValidatorValidatedJsonConverter<_, PlainValue.Validator<_>>>.MakeGenericType(typeArguments)
        Activator.CreateInstance(converterType) :?> JsonConverter

    override this.CanConvert(typeToConvert) =
        typeToConvert.IsGenericType
        && (let typeToConvert = if typeToConvert.IsGenericTypeDefinition then typeToConvert else typeToConvert.GetGenericTypeDefinition()
            typeToConvert.Equals(typedefof<NewCtorAsyncValidatorValidated<_, PlainValue.Validator<_>>>))

type WriteOnlyInstanceValidatorValidatedJsonConverter<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>>() =
    inherit JsonConverter<InstanceValidatorValidated<'value, 'validator>>()

    override this.Write(writer, value, options) =
        JsonSerializer.Serialize(writer, value.Value, options)

    override this.Read(reader, typeToConvert, options) =
        invalidOp "InstanceValidatorValidated value cannot be read from Json. Not supported."

type WriteOnlyInstanceValidatorValidatedJsonConverterFactory() =
    inherit JsonConverterFactory()

    override this.CreateConverter(typeToConvert, options) =
        let typeArguments = typeToConvert.GetGenericArguments()
        let converterType = typedefof<WriteOnlyInstanceValidatorValidatedJsonConverter<_, PlainValue.Validator<_>>>.MakeGenericType(typeArguments)
        Activator.CreateInstance(converterType) :?> JsonConverter

    override this.CanConvert(typeToConvert) =
        typeToConvert.IsGenericType
        && (let typeToConvert = if typeToConvert.IsGenericTypeDefinition then typeToConvert else typeToConvert.GetGenericTypeDefinition()
            typeToConvert.Equals(typedefof<InstanceValidatorValidated<_, PlainValue.Validator<_>>>))

type WriteOnlyInstanceAsyncValidatorValidatedJsonConverter<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>>() =
    inherit JsonConverter<InstanceAsyncValidatorValidated<'value, 'validator>>()

    override this.Write(writer, value, options) =
        JsonSerializer.Serialize(writer, value.Value, options)

    override this.Read(reader, typeToConvert, options) =
        invalidOp "InstanceAsyncValidatorValidated value cannot be read from Json. Not supported."

type WriteOnlyInstanceAsyncValidatorValidatedJsonConverterFactory() =
    inherit JsonConverterFactory()

    override this.CreateConverter(typeToConvert, options) =
        let typeArguments = typeToConvert.GetGenericArguments()
        let converterType = typedefof<WriteOnlyInstanceAsyncValidatorValidatedJsonConverter<_, PlainValue.Validator<_>>>.MakeGenericType(typeArguments)
        Activator.CreateInstance(converterType) :?> JsonConverter

    override this.CanConvert(typeToConvert) =
        typeToConvert.IsGenericType
        && (let typeToConvert = if typeToConvert.IsGenericTypeDefinition then typeToConvert else typeToConvert.GetGenericTypeDefinition()
            typeToConvert.Equals(typedefof<InstanceAsyncValidatorValidated<_, PlainValue.Validator<_>>>))

[<AutoOpen>]
module JsonSerializerOptionsExtensions =
    type JsonSerializerOptions with
        member this.AddValidatedConverters() =
           this.Converters.Add(NewCtorValidatorValidatedJsonConverterFactory())
           this.Converters.Add(WriteOnlyNewCtorAsyncValidatorValidatedJsonConverterFactory())
           this.Converters.Add(WriteOnlyInstanceValidatorValidatedJsonConverterFactory())
           this.Converters.Add(WriteOnlyInstanceAsyncValidatorValidatedJsonConverterFactory())
           this